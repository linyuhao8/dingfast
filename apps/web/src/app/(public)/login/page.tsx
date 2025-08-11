"use client";
//React
import { useState, useEffect, FormEvent } from "react";

//nextjs
import { useRouter } from "next/navigation";

//hook auth direct
import useAuth from "@/hooks/auth/useAuth";

//toast
import toast from "react-hot-toast";
import Button from "@/components/common/ui/Button";
//components
import Title from "@/components/public/login/Title";
import Input from "@/components/public/login/Input";
import Forgot from "@/components/public/login/Forgot";
import Register from "@/components/public/login/Register";
import SocialLogin from "@/components/public/login/SocialLogin";
import Navbar from "@/components/public/Navbar";

//axios
import axios, { AxiosError } from "axios";
import { User } from "@/types/user";
import { ApiResponse } from "@/types/api-response";

export default function Login() {
  const router = useRouter();
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  useEffect(() => {
    console.log(email, password);
  }, [email, password]);
  // hook check auth
  const { isAuthenticated, user } = useAuth();

  //If you're logged in and directed to the dash
  useEffect(() => {
    if (isAuthenticated === true && user) {
      toast("You are logged in.", { icon: "⚠️" });

      let targetPath = "/";
      if (user.role === "Merchant") {
        targetPath = "/merchant/dashboard";
      } else if (user.role === "Admin") {
        targetPath = "/admin/dashboard";
      }

      // 使用 replace 可防止按 back 回到 login 頁
      router.replace(targetPath);
    }
  }, [isAuthenticated, user, router]);

  //Submit form
  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    toast.loading("Logging in...");

    try {
      setLoading(true);
      console.log(process.env.NEXT_PUBLIC_API_URL, "/api/auth/login");
      console.log({ email, password });

      const response = await axios.post<ApiResponse<User>>(
        `${process.env.NEXT_PUBLIC_API_URL}/api/auth/login`,
        { Email: email, Password: password }, // 注意這裡大寫開頭
        { withCredentials: true }
      );

      const result = response.data;
      console.log("now", result);
      toast.dismiss();

      if (result.success && result.data) {
        toast.success(result.message || "Login successful!");

        const { role } = result.data;

        if (role === "Merchant") {
          router.push("/merchant/dashboard");
        } else if (role === "Admin") {
          router.push("/admin/dashboard");
        } else {
          router.push("/");
        }
      } else {
        toast.error(result.message || "Login failed, please try again.");
      }
    } catch (err) {
      toast.dismiss();
      const error = err as AxiosError<ApiResponse<null>>;
      toast.error(
        error.response?.data?.message || error.message || "Login failed."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Navbar />
      <div className="min-h-[calc(100vh-70px)] bg-stone-50 dark:bg-gray-700 flex flex-col justify-center">
        <main className="flex flex-col items-center px-4">
          <div className="w-full max-w-md bg-white dark:bg-gray-800 rounded-2xl shadow-md overflow-hidden">
            <Title />

            <form onSubmit={handleSubmit} className="px-8 pb-10">
              <Input
                email={email}
                password={password}
                setEmail={setEmail}
                setPassword={setPassword}
              />
              <Forgot />
              <Button
                variant="full"
                disabled={loading}
                disabledText="login..."
                type="submit"
              >
                Login
              </Button>
              <SocialLogin />
              <Register />
            </form>
          </div>
        </main>
      </div>
    </>
  );
}
