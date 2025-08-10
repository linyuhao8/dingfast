import React from "react";
import { ChevronDown } from "lucide-react";

type SelectOption = {
  id?: string | number;
  value?: string | number;
  name?: string;
  label?: string;
  type?: string;
};

type InputFieldType =
  | "text"
  | "email"
  | "password"
  | "number"
  | "select"
  | "radio"
  | "checkbox"
  | "textarea";

interface InputFieldProps {
  id: string;
  name: string;
  type?: InputFieldType;
  value?: string | number;
  onChange?: (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => void;
  placeholder?: string;
  required?: boolean;
  className?: string;
  options?: SelectOption[]; // for select
  checked?: boolean; // for checkbox/radio
  variant?: string;
  label?: string;
  icon?: React.ComponentType<React.SVGProps<SVGSVGElement>>;
  selectPlaceholder?: string;
  rows?: number; // for textarea
  autoComplete?: string;
}

export default function InputField({
  id,
  name,
  type = "text",
  value,
  onChange,
  placeholder,
  required = false,
  className = "",
  options = [],
  checked,
  label,
  icon: Icon,
  selectPlaceholder = "-- 請選擇 Option --",
  rows = 4,
  autoComplete = "on",
}: InputFieldProps) {
  const baseInputStyle =
    "w-full bg-white dark:bg-gray-700 border border-gray-300 dark:border-gray-600 rounded-md px-4 py-2 text-gray-700 dark:text-gray-300 placeholder-gray-400 dark:placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-orange-300 dark:focus:ring-orange-300 focus:border-transparent";

  const checkboxRadioWrapperStyle =
    "flex items-center p-3 border border-gray-200 dark:border-gray-600 rounded-lg cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors";

  const checkboxRadioInputStyle =
    "w-4 h-4 text-orange-300 focus:ring-orange-300 dark:focus:ring-orange-300";
  const renderInput = () => {
    switch (type) {
      case "select":
        return (
          <div className="relative">
            <select
              id={id}
              name={name}
              value={value}
              onChange={onChange as React.ChangeEventHandler<HTMLSelectElement>}
              required={required}
              className={`${baseInputStyle} appearance-none pr-10 ${className}`}
            >
              <option value="">{selectPlaceholder}</option>
              {options.map((opt) => (
                <option key={opt.value ?? opt.id} value={opt.value ?? opt.id}>
                  {opt.label ?? opt.name} {opt.type && `(${opt.type})`}
                </option>
              ))}
            </select>
            <ChevronDown className="absolute right-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400 pointer-events-none" />
          </div>
        );

      case "radio":
      case "checkbox":
        return (
          <label className={`${checkboxRadioWrapperStyle} ${className}`}>
            <input
              id={id}
              name={name}
              type={type}
              checked={checked}
              value={value}
              onChange={onChange as React.ChangeEventHandler<HTMLInputElement>}
              required={required}
              className={`${checkboxRadioInputStyle} ${
                type === "checkbox" ? "rounded" : ""
              }`}
              autoComplete={autoComplete}
            />
            {Icon && (
              <Icon className="w-4 h-4 mx-3 text-gray-500 dark:text-gray-400" />
            )}
            <span className="ml-2 text-gray-700 dark:text-gray-300">
              {label}
            </span>
          </label>
        );

      case "textarea":
        return (
          <textarea
            id={id}
            name={name}
            value={value}
            onChange={onChange as React.ChangeEventHandler<HTMLTextAreaElement>}
            required={required}
            placeholder={placeholder}
            rows={rows}
            className={`${baseInputStyle} resize-vertical ${className}`}
          />
        );

      case "number":
      case "text":
      case "email":
      case "password":
      default:
        return (
          <input
            id={id}
            name={name}
            type={type}
            value={value}
            onChange={onChange as React.ChangeEventHandler<HTMLInputElement>}
            required={required}
            placeholder={placeholder}
            autoComplete="off"
            className={`${baseInputStyle} ${className}`}
          />
        );
    }
  };

  return (
    <div>
      {type !== "radio" && type !== "checkbox" && label && (
        <label
          htmlFor={id}
          className="text-sm font-medium text-gray-700 dark:text-gray-300"
        >
          {label} {required && <span className="text-red-500">*</span>}
        </label>
      )}
      {renderInput()}
    </div>
  );
}
