import type { Actor } from "./actor";

export type Aggregate = {
  version: number;
  createdBy: Actor;
  createdOn: string;
  updatedBy: Actor;
  updatedOn: string;
};
