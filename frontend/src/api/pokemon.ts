import type { PokemonType, SearchPokemonTypesPayload } from "@/types/pokemon";
import type { SearchResults } from "@/types/search";
import { post } from ".";

export async function searchPokemonTypes(payload: SearchPokemonTypesPayload): Promise<SearchResults<PokemonType>> {
  return (await post<SearchPokemonTypesPayload, SearchResults<PokemonType>>("/pokemon/types/search", payload)).data;
}
