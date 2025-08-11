"use client";

import { useState, useEffect } from "react";
import { api } from "@/api";
import { getCookie, setCookie, deleteCookie } from "cookies-next/client";
import { User } from "@/types/user";
import { ApiResponse } from "@/types/api-response";

const useAuth = () => {
  // 是否已通過認證，初始設 null 表示還沒判斷
  const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null);

  // 用戶資料，初始為 null
  const [user, setUser] = useState<User | null>(null);

  // 載入狀態
  const [isLoading, setIsLoading] = useState<boolean>(true);

  // 錯誤訊息
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    // Step 1：從 cookie 嘗試讀取用戶資訊
    const userCookie = getCookie("order-user");
    if (userCookie) {
      try {
        const parsed: User = JSON.parse(userCookie as string);
        setUser(parsed);
        console.log("cookie", parsed);
        setIsAuthenticated(true);
        setIsLoading(true); // 雖有 cookie 但仍須 call API 確認
      } catch (err) {
        // 解析失敗，刪除 cookie
        deleteCookie("order-user");
        console.log(err);
      }
    }

    // Step 2：呼叫 API 確認用戶身份
    const checkAuth = async () => {
      try {
        const response = await api.auth.checkAuth();

        if (!response) {
          handleUnauthenticated("No response from server");
          return;
        }

        const data: ApiResponse<User> = response;

        if (!isMounted) return;

        if (data.success && data.data) {
          setIsAuthenticated(true);
          setUser(data.data);
          setCookie("order-user", JSON.stringify(data.data), {
            maxAge: 60 * 60 * 24 * 7,
            path: "/",
          });
        } else {
          handleUnauthenticated();
        }
      } catch (err) {
        if (!isMounted) return;
        handleUnauthenticated("Auth failed");
        console.error(err);
      } finally {
        if (isMounted) setIsLoading(false);
      }
    };

    // Step 3：未通過認證時的處理
    const handleUnauthenticated = (msg = "Unauthorized") => {
      setIsAuthenticated(false);
      setUser(null);
      setError(msg);
      deleteCookie("order-user");
      deleteCookie("order-merchant");
    };

    checkAuth();

    return () => {
      isMounted = false;
    };
  }, []);

  return {
    isAuthenticated,
    user,
    isLoading,
    error,
  };
};

export default useAuth;
