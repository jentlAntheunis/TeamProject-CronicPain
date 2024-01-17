import { defineConfig } from "vite";
import { VitePWA } from "vite-plugin-pwa";
import react from "@vitejs/plugin-react";

const manifestForPlugin = {
  registerType: "prompt",
  includeAssets: ["favicon.ico", "apple-touch-icon.png", "mask-icon.svg"],
  manifest: {
    name: "Pebbles",
    short_name: "Pebbles",
    description: "Stap voor stap naar een pijnvrij leven vol energie",
    theme_color: "#ffffff",
    icons: [
      {
        src: "android-chrome-192x192.png",
        sizes: "192x192",
        type: "image/png",
      },
      {
        src: "android-chrome-512x512.png",
        sizes: "512x512",
        type: "image/png",
      },
      {
        src: "apple-touch-icon.png",
        sizes: "180x180",
        type: "image/png",
      },
      {
        src: "favicon-16x16.png",
        sizes: "16x16",
        type: "image/png",
      },
      {
        src: "favicon-32x32.png",
        sizes: "32x32",
        type: "image/png",
      },
      {
        src: "maskable-icon.png",
        sizes: "196x196",
        type: "image/png",
        purpose: "maskable",
      },
    ],
    background_color: "#ffffff",
    display: "standalone",
    scope: "/",
    start_url: "/",
    orientation: "portrait",
  },
};

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), VitePWA(manifestForPlugin)],
});
