import Link from "next/link";
export default function Forgot() {
  return (
    <div className="mt-2 flex justify-end mb-5">
      <Link
        href="/forgot"
        className="text-sm font-medium text-amber-500 hover:text-amber-400 transition duration-150"
      >
        Forgot your password?
      </Link>
    </div>
  );
}
