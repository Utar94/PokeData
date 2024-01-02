import type { Aggregate } from "./aggregate";
import type { Region } from "./region";

export type Generation = Aggregate & {
  number: number;
  uniqueName: string;
  displayName?: string;
  mainRegion?: Region;
};
