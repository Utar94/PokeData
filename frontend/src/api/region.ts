import type { Region, SearchRegionsPayload } from "@/types/region";
import type { SearchResults } from "@/types/search";
import { post } from ".";

export async function searchRegions(payload: SearchRegionsPayload): Promise<SearchResults<Region>> {
  return (await post<SearchRegionsPayload, SearchResults<Region>>("/regions/search", payload)).data;
}
