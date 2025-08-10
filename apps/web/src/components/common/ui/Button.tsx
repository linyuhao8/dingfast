import Link from "next/link";
import clsx from "clsx";
import { FiLoader } from "react-icons/fi";
import React, { MouseEventHandler, ReactNode } from "react";

interface ButtonProps {
  href?: string;
  onClick?: MouseEventHandler<HTMLButtonElement>;
  children?: ReactNode;
  variant?: keyof typeof variants; // 之後會用 variants 物件的 key
  size?: keyof typeof sizes;
  icon?: React.ComponentType<{ className?: string }>;
  iconPosition?: "left" | "right";
  className?: string;
  disabled?: boolean;
  disabledText?: string;
  type?: "button" | "submit" | "reset";
}

const variants = {
  primary:
    "flex items-center justify-center bg-amber-500 text-white hover:bg-amber-400 rounded-lg",
  secondary:
    "flex items-center justify-center bg-gray-100 dark:bg-gray-600 text-gray-800 dark:text-white hover:bg-gray-200 dark:hover:bg-gray-500",
  outline:
    "inline-flex items-center justify-center border border-gray-100 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white dark:bg-gray-800 dark:border-gray-700 dark:text-gray-200 hover:bg-gray-50 dark:hover:bg-gray-700 focus:outline-none focus:ring-1 focus:ring-offset-1 focus:ring-amber-500",
  danger:
    "flex items-center justify-center bg-red-500 text-white hover:bg-red-400",
  transparently:
    "flex items-center justify-center flex-nowrap bg-transpanent text-lg",
  icon: "w-9 h-9 rounded-full flex items-center justify-center bg-gray-200 dark:bg-gray-600 text-gray-600 dark:text-gray-300 cursor-pointer",
  activeTab:
    "whitespace-nowrap py-4 px-1 border-b-2 font-medium text-sm border-amber-500 text-amber-600",
  tab: "whitespace-nowrap py-4 px-1 border-b-2 font-medium text-sm border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300",
  square:
    "flex justify-center items-center h-9 w-9 rounded-lg bg-gray-100 text-gray-600 hover:bg-amber-100 hover:text-amber-600 transition-colors dark:bg-gray-700 dark:text-gray-300 dark:hover:bg-amber-900/30 dark:hover:text-amber-400",
  dashboardNav:
    "text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 hover:text-gray-900 dark:hover:text-white flex flex-nowrap items-center gap-2",
  ghost:
    "text-gray-500 hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-800",
  full: "w-full bg-gradient-to-r from-orange-400 to-orange-500 hover:from-orange-500 hover:to-orange-600 text-white font-medium py-2 px-4 rounded-lg disabled:opacity-50 disabled:cursor-not-allowed transition-all duration-200 transform hover:scale-[1.02] active:scale-[0.98] shadow-lg hover:shadow-xl",
};

const sizes = {
  sm: "px-2 text-sm",
  md: "px-3 py-1 text-base",
  lg: "px-4 py-2 text-lg",
  icon: "px-2",
  none: "",
};

const Button: React.FC<ButtonProps> = ({
  href,
  onClick,
  children,
  variant = "primary",
  size = "md",
  icon: Icon,
  iconPosition = "left",
  className = "",
  disabled = false,
  disabledText = "loading",
  ...props
}) => {
  const baseStyles = "gap-1 font-medium transition cursor-pointer";

  const classes = clsx(baseStyles, variants[variant], sizes[size], className, {
    "opacity-50 cursor-not-allowed": disabled,
  });

  const content =
    disabled && disabledText ? (
      <span className="flex items-center justify-center gap-2">
        <FiLoader className="animate-spin" />
        {disabledText}
      </span>
    ) : (
      <>
        {iconPosition === "left" && Icon && <Icon className="inline-block" />}
        {children}
        {iconPosition === "right" && Icon && <Icon className="inline-block" />}
      </>
    );

  return href ? (
    <Link href={href} className={classes} {...props}>
      {content}
    </Link>
  ) : (
    <button
      onClick={onClick}
      className={classes}
      disabled={disabled}
      {...props}
    >
      {content}
    </button>
  );
};

export default Button;
