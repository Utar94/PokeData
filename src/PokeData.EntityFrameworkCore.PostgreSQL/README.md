# PokeData.EntityFrameworkCore.PostgreSQL

TODO

## Migrations

This project is setup to use migrations. All the commands below must be executed in the solution directory.

### Create a migration

To create a new migration, execute the following command. Do not forget to provide a migration name!

`dotnet ef migrations add <YOUR_MIGRATION_NAME> --context PokemonContext --project src/PokeData.EntityFrameworkCore.PostgreSQL --startup-project src/PokeData`

### Remove a migration

To remove the latest unapplied migration, execute the following command.

`dotnet ef migrations remove --context PokemonContext --project src/PokeData.EntityFrameworkCore.PostgreSQL --startup-project src/PokeData`

### Generate a script

To generate a script, execute the following command. Do not forget to provide a source migration name!

`dotnet ef migrations script <SOURCE_MIGRATION> --context PokemonContext --project src/PokeData.EntityFrameworkCore.PostgreSQL --startup-project src/PokeData`
