import { defineShikiSetup } from '@slidev/types'
import { bundledThemes } from 'shiki'

export default defineShikiSetup(async () => {
  return {
    theme: await bundledThemes.monokai(),
  }
})
