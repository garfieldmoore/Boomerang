require "rexml/document"
include REXML

# set properties
programFilesPath=""
buildConfiguration = "release"
productName="VantageEba"
VsSolutionFile = "lattice.services.web.sln"
workingDir=Dir.pwd
packageDir = "buildArtifacts/bin"
installersPath = "buildArtifacts/installers/Installer.zip"
windirectory = ENV['windir']
msbuild = "#{windirectory}\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"
nunitconsole="packages/NUnit.2.5.10.11092/tools/nunit-console.exe"
nugetPath= 'packages_manual/nuget.exe'
zipExe = "packages_manual/7zip/7z.exe"
os_platform="x64"

task :default => :build
task :build => [ :clean, :envConfig, :compile, :package, :createInstallers]
task :commitBuild => [ :clean, :compile, :runTests, :codeAnalysis, :package, :createInstallers]

task :clean do
	FileUtils.rm_rf("buildartifacts")	
	Dir::mkdir("buildArtifacts")
end

task :envConfig do 
	puts "Starting rake task: envConfig"
	nugetParams = '-o packages'
	packages = Dir.glob("source/**/packages.config")+Dir.glob("tests/**/packages.config")
	packages.each do |package | 
		sh "#{nugetPath} install #{package} #{nugetParams}" 
	end
end

task :compile do
	puts "starting rake task: compile."
	
		componentName=VsSolutionFile.sub('.sln','')
		outputPath = "Components"

		signAssembly = false
		if (componentName.include? 'tests') || (componentName.include? 'Fake') || (componentName.include? 'TestDrivers')
			signAssembly = false
		end
		
		if ! componentName.include? 'Proxies'
			puts "Building:"
			puts "Source: #{componentName}"
			puts "Destination: #{outputPath}"
			params = " /t:Clean /t:Build /nologo /v:m /p:OutputPath=..\\..\\buildArtifacts\\bin\\#{outputPath} /p:Configuration=#{buildConfiguration} /p:StyleCopTreatErrorsAsWarnings=false #{VsSolutionFile} /p:SignAssembly=#{signAssembly} /p:AssemblyOriginatorKeyFile=..\\..\\build\\project.snk /p:DebugSymbols=true /p:DebugType=pdbonly"
			sh "#{msbuild} #{params}"
		end
	end

task :runTests do
	if (ENV['TEAMCITY_JRE'] !=nil) then	
		puts "Skipping unit tests: build server has built in steps for unit tests."
	else
		if ! FileTest::directory?("buildArtifacts/Reports")
			puts "Creating reports folder"
			Dir::mkdir("buildArtifacts/Reports")
		end
		
		puts "Looking for tests"
		testAssemblies = Dir.glob("buildArtifacts/bin/tests/**/*tests*.dll")
		testAssemblies.delete_if do |c| c.include?(".Tests.Acceptance") end
		
		testAssemblyCmd = testAssemblies.join(" ")
		puts "Test assemblies found.  Running tests in following assemblies;"
		puts testAssemblies
		params='/xml=buildArtifacts/Reports/TestResults.xml /exclude:Acceptance'
		sh "#{nunitconsole}" " #{testAssemblyCmd} #{params}"		
	end
end

task :package do
	projects = Dir.glob('source/**/*.web.csproj')+Dir.glob('tests/**/*.web.*.csproj')	
	puts "Projects to package:"
	puts projects
	
	projects.each do|project|
		params = " #{project} /T:Package /p:configuration=#{buildConfiguration};outputpath=../../buildartifacts/bin/Packages"
		sh "#{msbuild} #{params}"			
	end
end

task :createInstallers do	
	if File.file?(installersPath)
		File.delete(installersPath)
	end
	zipFolder("#{zipExe}", "#{packageDir}", "#{installersPath}")	
end

def zipFolder(zipExe, source, target)
	sh "#{zipExe} a #{target} .\\#{source}\\*"
	sh "#{zipExe} d #{target} tests/**.Tests.Unit"
end
