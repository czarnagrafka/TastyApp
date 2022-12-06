const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/restaurants",
      "/api/cuisines"
    ],
    target: "https://localhost:7260",
    secure: false,
    logLevel: "debug"
  }
]

module.exports = PROXY_CONFIG;
