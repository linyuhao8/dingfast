import { ApiResponse } from "@/types/api";
import { User } from "@/types/user";
// services/api/auth.js
export const authApi = {
  checkAuth: async (): Promise<ApiResponse<User>> => {
    const res = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/api/auth/check-auth`,
      {
        method: "GET",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
      }
    );
    const data = await res.json();
    return data;
  },
  // 同理 logout 也回傳 Response
  logout: async (): Promise<ApiResponse<string>> => {
    const res = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/api/auth/logout`,
      {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
      }
    );
    const data = await res.json();
    return data;
  },
};
