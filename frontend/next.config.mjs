import path from "path";
const nextConfig = {
  sassOptions: {
    includePaths: [path.join(process.cwd(), "styles")],
  },
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "images.pexels.com",
        pathname: "**",
      },
      {
        protocol: "https",
        hostname: "www.pexels.com",
        pathname: "**",
      },
    ],
  },
  output: "standalone",
};

export default nextConfig;
