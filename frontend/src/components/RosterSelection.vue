<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";
import { computed, ref } from "vue";

import PokemonTypeSelect from "./PokemonTypeSelect.vue";
import RegionSelect from "./RegionSelect.vue";
import RosterItem from "./RosterItem.vue";
import SearchTextInput from "./SearchTextInput.vue";
import type { RosterItem as RosterItemType } from "@/types/roster";

const props = defineProps<{
  items: RosterItemType[];
}>();

const count = ref<number>(100);
const region = ref<string>();
const search = ref<string>();
const type = ref<string>();

const filteredItems = computed<RosterItemType[]>(() =>
  props.items
    .filter((item) => {
      const terms: string[] = search.value?.split(" ") ?? [];
      if (terms.length) {
        for (const term of terms) {
          const pattern = new RegExp(term, "i");
          if (
            !pattern.test(item.source.name) &&
            (!item.source.category || !pattern.test(item.source.category)) &&
            (!item.destination || (!pattern.test(item.destination.name) && (!item.destination.category || !pattern.test(item.destination.category))))
          ) {
            return false;
          }
        }
      }

      const types = [item.source.primaryType, item.source.secondaryType ?? "", item.destination?.primaryType ?? "", item.destination?.secondaryType ?? ""]
        .filter((type) => type.length > 0)
        .map((type) => type.toLowerCase());
      if (type.value && !types.includes(type.value)) {
        return false;
      }

      if (region.value && item.source.region.toLowerCase() !== region.value && item.destination?.region.toLowerCase() !== region.value) {
        return false;
      }

      return true;
    })
    .slice(0, count.value),
);

defineEmits<{
  (e: "removed", pokemon: RosterItemType): void;
  (e: "selected", pokemon: RosterItemType): void;
}>();
</script>

<template>
  <div>
    <div class="row">
      <div class="col">
        <SearchTextInput id="search" v-model="search" />
      </div>
      <div class="col">
        <PokemonTypeSelect clear id="type-selection" v-model="type" />
      </div>
      <div class="col">
        <RegionSelect clear id="region-selection" v-model="region" />
      </div>
    </div>
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">Source</th>
          <th scope="col">Destination</th>
          <th scope="col">
            <div class="text-end">Actions</div>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in filteredItems" :key="item.speciesId">
          <td>
            <RosterItem :pokemon="item.source" />
          </td>
          <td>
            <RosterItem v-if="item.destination" :pokemon="item.destination" />
            <template v-else>N/A</template>
          </td>
          <td>
            <div class="text-end">
              <template v-if="item.destination">
                <TarButton class="me-2" :icon="['fas', 'pen-to-square']" text="Edit" variant="primary" @click="$emit('selected', item)" />
                <TarButton :icon="['fas', 'trash-can']" text="Remove" variant="danger" @click="$emit('removed', item)" />
              </template>
              <TarButton v-else :icon="['fas', 'plus']" text="Add" variant="success" @click="$emit('selected', item)" />
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="count <= items.length" class="text-center">
      <TarButton :icon="['fas', 'plus']" size="large" text="Show more results" @click="count += 100" />
    </div>
    <div class="mb-3"></div>
  </div>
</template>
