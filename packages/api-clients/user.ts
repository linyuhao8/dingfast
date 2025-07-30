// api-clients/user.ts
import axios from "axios";
import { CreateUserDto } from "../shared-dtos-ts/user/UserDto";

export async function createUser(data: CreateUserDto) {
  const res = await axios.post("/api/users/create", data);
  return res.data;
}
