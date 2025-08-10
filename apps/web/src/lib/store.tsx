// store.ts 或 store.js
import { configureStore } from "@reduxjs/toolkit";
import themeReducer from "@/lib/slices/themeSlice";
import merchantReducer from "@/lib/slices/merchantSlice";

const store = configureStore({
  reducer: {
    theme: themeReducer,
    merchant: merchantReducer,
  },
});

export default store;
