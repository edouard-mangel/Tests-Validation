<script setup lang="ts">
import { computed } from 'vue'
import { handleBackground } from '../utils/layoutHelper'
const props = defineProps({
  background: {
    default: '',
  },
})

const style = computed(() => handleBackground(props.background, true))
</script>

<template>
  <div class="slidev-layout end h-full grid place-content-center px-12" :style="style">
    <div class="my-auto flex flex-col justify-center items-center text-center gap-4 w-full">
      <slot />
    </div>
  </div>
</template>
