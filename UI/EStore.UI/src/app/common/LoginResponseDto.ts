import { UserDto } from "./UserDto";

export class LoginResponseDto {
    user: UserDto;
    token: string;
  }