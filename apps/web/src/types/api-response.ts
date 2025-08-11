export interface ApiResponse<T> {
  success: boolean;
  data: T | null;
  message?: string;
  errorCode?: number;
  errorDetail?: string;
}
