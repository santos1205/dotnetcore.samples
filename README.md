# DOTNET CLI commands

1 - scaffold base - base first.

dotnet ef dbcontext scaffold "data source=sqldesenv.proseg.com.br;initial catalog=<DB NAME>;user id=proseg;password=b123;MultipleActiveResultSets=True;App=EntityFramework" Microsoft.EntityFrameworkCore.SqlServer -o Models
  
2 - scaffold controller / views
  
dotnet aspnet-codegenerator controller -name \<NAME CONTROLLER\>Controller -m \<NAME MODEL\> -dc \<NAME OF CREATED CONTEXT\> --relativeFolderPath Controllers --useDefaultLayout

3 - Update startup.cs file:

in ConfigureService:

services.AddDbContext<EstudoContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("<NAME OF CONTEXT>")));

4 - Update appsettings.json file:

below "AllowedHosts":

"ConnectionStrings": {    
    "<NAME OF CONTEXT>": "data source=sqldesenv.proseg.com.br;initial catalog=<NAME OF BASE>Estudo;user id=proseg;password=b123;MultipleActiveResultSets=True;App=EntityFramework"
  }

----------------------------------------------------------------------------------------------------------------
vs code short cuts 
ctrl + d: change same word at same time
alt + upArrow: move line up
alt + downArrow: move line down
ctrl + ; - comment line
shit + alt + downArrow - copy line down
shift + alt + upArrow - copy line up
shift + alt + f - format document


4 - publish command

dotnet publish XXXXXXXXXXX.csproj -c debug --self-contained -r win10-x64
