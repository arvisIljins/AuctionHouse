import path from "path";
const nextConfig = {
  experimental: {
    serverActions: true,
  },
  sassOptions: {
    includePaths: [path.join(process.cwd(), "styles")],
  },
  images: {
    domains: ["images.pexels.com", "www.pexels.com"],
  },
  output: "standalone",
};

export default nextConfig;
