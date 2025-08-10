import InputField from "@/components/common/ui/InputField";

interface FromInputProps {
  email: string;
  password: string;
  setEmail: React.Dispatch<React.SetStateAction<string>>;
  setPassword: React.Dispatch<React.SetStateAction<string>>;
}

export default function FormInput({
  email,
  password,
  setEmail,
  setPassword,
}: FromInputProps) {
  return (
    <div className="input">
      <InputField
        id="email"
        name="email"
        type="email"
        autoComplete="email"
        placeholder="Email"
        required={true}
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <div className="mt-3">
        <InputField
          id="password"
          name="password"
          type="password"
          autoComplete="current-password"
          required
          placeholder="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
    </div>
  );
}
