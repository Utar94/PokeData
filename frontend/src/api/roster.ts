import type { PokemonRoster, SaveRosterItemPayload } from "@/types/roster";
import { get, put } from ".";

export async function readRoster(): Promise<PokemonRoster> {
  return (await get<PokemonRoster>("/pokemon/roster")).data;
}

export async function saveRosterItem(speciesId: number, payload: SaveRosterItemPayload): Promise<void> {
  await put(`/pokemon/roster/${speciesId}`, payload);
}
