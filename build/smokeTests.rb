# tests application services are working following a deployment
require "net/http"

DB_MAJOR="6"
DB_MINOR="0"
DB_REVISION ="12"
DB_BUILD="0"

task :CheckDatabaseAccess do	
	puts "Starting task:CheckDatabaseAccess"
	sqlCmd = ENV['SQLCMDPATH']
	params = "-o CheckDataBaseOutput.txt -U SN_ADMIN -P ke%f73Xe5N0 -d IM_MONEY -Q \"SELECT [Major], [Minor], [Revision], [Build] FROM [IM_Money].[dbo].[SchemaVersion] WHERE [Revision] = (select MIN([Revision]) FROM [IM_Money].[dbo].[SchemaVersion]);\""
	sh "#{sqlCmd} #{params}"
	
	file = File.open('CheckDataBaseOutput.txt')
	contents = file.readline
	contents = file.readline
	contents = file.readline
	contents.squeeze!(' ')
	schemaVersion = contents.split
	
	puts "Database schema version:"
	puts "Major:'#{schemaVersion[0]}'"
	puts "Minor:'#{schemaVersion[1]}'"
	puts "Revision:'#{schemaVersion[2]}'"
	puts "Build:'#{schemaVersion[3]}'"

	if (DB_MAJOR != schemaVersion[0] || DB_MINOR != schemaVersion[1] || DB_REVISION !=schemaVersion[2] || DB_BUILD !=schemaVersion[3])
		puts "Smoke tests failed: Unexpected database schema;"
		exit(-1)
		raise
	end

	puts "database access response:ok"
end

task :CheckWebsiteAccess, :domain, :expectedHttpCode, :url do |t, args|
	puts "Starting task:CheckWebsiteAccess"
	puts args
	urlStrings=args[:url].split(';')
	domain=args[:domain]
	expectedHttpCode=args[:expectedHttpCode]
	
	urlStrings.each do|urlString|
		uri = URI.parse("#{domain}#{urlString}")
		http = Net::HTTP.new(uri.host, uri.port)
		request = Net::HTTP::Get.new(uri.request_uri)
		request.initialize_http_header({"User-Agent" => "Smoke Script"})
		
		puts "Checking access to #{uri}"
		#check response
		result = -1
		begin
			response = http.request(request)
			puts "Server response: " + response.code
			if response.code.to_s <= "#{expectedHttpCode}"
				puts "Access ok"
				result = 0
			else
				puts "Request to #{urlString} failed"
			end
		rescue
			puts "Request to #{urlString} failed"
		end
		
		if result != 0
			exit(result)
			raise
		end
	end
end
