param(
	[ValidateSet('development', 'staging', 'production')][String]$environment = "development",
	[bool]$build = $True
)

If($build){
	docker-compose build
}

If($environment -eq "development"){
	docker-compose up -d
} Else{
	docker-compose -f docker-compose.yml -f docker-compose.$environment.yml up -d
}