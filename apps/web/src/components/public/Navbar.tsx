"use client";
import Link from "next/link";

// Icons
import { FaUser } from "react-icons/fa";
import { MdSpaceDashboard } from "react-icons/md";

// Hooks
import useAuth from "@/hooks/auth/useAuth";

// Component
import Button from "@/components/common/ui/Button";
// import LogoutButton from "@/components/common/auth/LogoutButton";
// import ThemeButton from "../common/ui/ThemeButton";

// isAuthenticated display
const UserLinks = () => (
  <div className="flex space-x-2 items-center">
    <Button href="/account" variant="icon" icon={FaUser}></Button>
    <Button href="/orders" variant="icon" icon={MdSpaceDashboard}></Button>
    {/* <LogoutButton variant="transparently" size="none" isHome="true" /> */}
  </div>
);
// unAuthenticated display
const GuestLinks = () => <Button href="/login">Login</Button>;

const Navbar = () => {
  // hook check auth
  const { isAuthenticated } = useAuth();

  return (
    <div className="flex justify-between items-center px-3 py-5">
      <Link href="/">
        <div className="flex items-center flex-nowrap space-x-2">
          <div className="bg-amber-500 text-white rounded-full h-8 w-8 flex items-center justify-center">
            <span>H</span>
          </div>
          <div>
            <span className="font-medium">Hank Order</span>
          </div>
        </div>
      </Link>

      <div className="flex flex-nowrap gap-2">
        {/* Theme toggle */}
        {/* <ThemeButton /> */}
        {/* User or Guest links */}
        {isAuthenticated ? <UserLinks /> : <GuestLinks />}
      </div>
    </div>
  );
};

export default Navbar;
