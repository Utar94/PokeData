import type { PokemonRoster, SaveRosterItemPayload, SavedRosterItem } from "@/types/roster";
import { _delete, get, put } from ".";

export async function readRoster(): Promise<PokemonRoster> {
  return (await get<PokemonRoster>("/pokemon/roster")).data;
}

export async function removeRosterItem(speciesId: number): Promise<SavedRosterItem> {
  return (await _delete<SavedRosterItem>(`/pokemon/roster/${speciesId}`)).data;
}

export async function saveRosterItem(speciesId: number, payload: SaveRosterItemPayload): Promise<SavedRosterItem> {
  return (await put<SaveRosterItemPayload, SavedRosterItem>(`/pokemon/roster/${speciesId}`, payload)).data;
}
