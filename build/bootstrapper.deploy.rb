task :default => :runDeploy

task :runDeploy do
    deployDir="buildArtifacts/deployables/_PublishedWebsites"
    sh "rake -f #{deployDir}/deploy.rb -t deploySite[\"#{deployDir}, localHost, sa, Irdog247, C:/inetpub/wwwroot\"]"
end