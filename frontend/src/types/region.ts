import type { Aggregate } from "./aggregate";
import type { Generation } from "./generation";
import type { SearchPayload, SortOption } from "./search";

export type Region = Aggregate & {
  number: number;
  uniqueName: string;
  displayName?: string;
  mainGeneration?: Generation;
};

export type RegionSort = "DisplayName" | "Number" | "UniqueName" | "UpdatedOn";

export type RegionSortOption = SortOption & {
  field: RegionSort;
};

export type SearchRegionsPayload = SearchPayload & {
  numberIn: number[];
  sort: RegionSortOption[];
};
