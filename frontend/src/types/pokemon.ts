import type { Aggregate } from "./aggregate";
import type { SearchPayload, SortOption } from "./search";

export type PokemonType = Aggregate & {
  number: number;
  uniqueName: string;
  displayName?: string;
};

export type PokemonTypeSort = "DisplayName" | "Number" | "UniqueName" | "UpdatedOn";

export type PokemonTypeSortOption = SortOption & {
  field: PokemonTypeSort;
};

export type SearchPokemonTypesPayload = SearchPayload & {
  numberIn?: number[];
  sort?: PokemonTypeSortOption[];
};
