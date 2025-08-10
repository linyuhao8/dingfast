export interface Image {
  // 依照你的 Image 類型定義，這裡先示範基本欄位
  id: number;
  url: string;
  // 其他欄位依需求補充
}

export type UserRole = "Customer" | "Admin" | "Merchant"; // 根據你的 UserRole enum 改成字串 union 或 enum

export interface User {
  name?: string; // 可選，string 或 undefined
  email: string; // 必填
  password: string; // 必填
  phoneNumber?: string; // 可選
  address?: string; // 可選
  role: UserRole; // 必填
  images: Image[]; // 陣列，非空但可空陣列
}
