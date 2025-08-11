//app/layout.js For merchant and public
import "@/styles/globals.css";
import { StoreProvider } from "@/lib/storeProvide";
import { Toaster } from "react-hot-toast";

export const metadata = {
  title: "Order",
  description: "order",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <StoreProvider>
          {/* Redux Store */}
          {children}
          <Toaster />
        </StoreProvider>
      </body>
    </html>
  );
}
