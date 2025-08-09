import { deleteCookie } from "cookies-next/client";
import { useMerchant } from "./useMerchant";
import { api } from "@/api";

export const useLogout = () => {
  const { clearCurrentMerchant } = useMerchant();

  const logout = async () => {
    try {
      const response = await api.auth.logout();
      // 這邊假設 api.auth.logout() 已經回傳解析後的 ApiResponse<T> 物件
      if (response.Success) {
        deleteCookie("order-user");
        clearCurrentMerchant();
        return { success: true, message: response.Message };
      } else {
        return {
          success: false,
          message: response.Message || "Logout Failure",
        };
      }
    } catch (err: any) {
      return {
        success: false,
        message: err?.message || "Logout Error",
      };
    }
  };

  return { logout };
};
