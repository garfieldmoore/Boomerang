require 'fileutils'
deployDir=""
params=""

webDeployPath="C:/inetpub/wwwroot/baseline.web.mvc3_deploy"
configTool="c:/dev/ConfigTool/imenvcfg.exe"

task :default do
	puts "Deploys the system to a remote host"
end

task :deploySite, :deployablesDir, :webAppPath do  |t, args|
	deployDir=args[:deployablesDir]
	webDeployPath=args[:webAppPath]
	Rake::Task["deploy"].invoke
end

task :deploy => [:environmentConfig, :runInstaller]

# environment config
task :environmentConfig do
end

task :runInstaller do	
	puts "Starting task:runInstaller"
	packages=Dir.glob("BuildArtifacts/**/_PublishedWebsites/**/*.deploy.cmd")
	params='/Y /M:localhost'
	packages.each do|package|
		puts "Deploying package:#{package}"
		sh "#{package} #{params}"
	end	
end

task :applicationConfig do
	puts "Starting task:applicationConfig"
	environments= ENV['BuildEnvironments'].split(';')
	if (environments!=nil)
        environment=environments[0]
		puts "Configuring components for environment:#{environment}"
		
		configs=Dir.glob("#{webDeployPath}/**/_Env.Config/#{environment}*.settings.env")
		configs.delete_if do |c| c.include?("/bin/") end
		
		puts "Configurations found at:#{webDeployPath}"
		puts  "pattern: #{webDeployPath}/**/_Env.Config/#{environment}*.settings.env"
		configs.each do |config|
			puts "#{config}"
		end
		configs.each do |config|
			target=config.sub("#{environment}.",'')
			target.sub!(".settings.env",'.Template')
			if (File.exists?(target) && File.exists?(config))
				puts "Transforming configuration: Source:#{config}, target:#{target}"
				sh "#{configTool} #{config} #{target}"	
				
				replaceFile=target.sub('_Env.Config/','')
				replaceFile.sub!('.Template','')
				puts "Copying files: Source:#{target}, target:#{replaceFile}"
				FileUtils.cp(target,replaceFile) 
			else
				puts "Transform files not found:\n\rsource:#{config} \n\rtarget:#{target}"
			end
		end		
	end
end