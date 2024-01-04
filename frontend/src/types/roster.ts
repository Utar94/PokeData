export type PokemonRoster = {
  items: RosterItem[];
  stats: RosterStatistics;
};

export type RosterInfo = {
  number: number;
  name: string;
  category?: string;
  region: string;
  primaryType: string;
  secondaryType?: string;
  isBaby: boolean;
  isLegendary: boolean;
  isMythical: boolean;
};

export type RosterItem = {
  speciesId: number;
  source: RosterInfo;
  destination?: RosterInfo;
};

export type RosterStatistic = {
  key: string;
  count: number;
  percentage: number;
};

export type RosterStatistics = {
  count: number;
  regions: RosterStatistic[];
  types: RosterStatistic[];
};

export type SaveRosterItemPayload = {
  number: number;
  name: string;
  category?: string;
  region: number;
  primaryType: number;
  secondaryType?: number;
  isBaby: boolean;
  isLegendary: boolean;
  isMythical: boolean;
};

export type SavedRosterItem = {
  item: RosterItem;
  stats: RosterStatistics;
};
