export interface UserDto {
  id: string;
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
  role: "Customer" | "Merchant" | "Admin";
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserDto {
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
  role: "Customer" | "Merchant" | "Admin";
}
