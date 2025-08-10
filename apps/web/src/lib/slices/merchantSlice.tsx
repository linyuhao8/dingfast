// slices/merchantSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { setCookie, deleteCookie } from "cookies-next/client";

interface Merchant {
  id: number;
  user_id: number;
  business_name: string;
  description: string;
  merchant_logo_id: number;
  feature: string;
  location: string;
  business_hours: string;
  is_active: boolean;
  image_id: number;
}

interface MerchantState {
  data: Merchant | null;
}

const initialState: MerchantState = {
  data: null,
};

const merchantSlice = createSlice({
  name: "merchant",
  initialState,
  reducers: {
    setMerchant: (state, action: PayloadAction<Merchant>) => {
      const {
        id,
        user_id,
        business_name,
        description,
        merchant_logo_id,
        feature,
        location,
        business_hours,
        is_active,
        image_id,
      } = action.payload;

      const merchantData = JSON.stringify({
        id,
        user_id,
        business_name,
        description,
        merchant_logo_id,
        feature,
        location,
        business_hours,
        is_active,
        image_id,
      });

      setCookie("order-merchant", merchantData, {
        maxAge: 60 * 60 * 24 * 7,
        path: "/",
      });
      state.data = action.payload;
    },
    clearMerchant: (state) => {
      deleteCookie("order-merchant", { path: "/" });
      state.data = null;
    },
  },
});

export const { setMerchant, clearMerchant } = merchantSlice.actions;
export default merchantSlice.reducer;
