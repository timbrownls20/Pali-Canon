interface IConfig {
  api: string;
  interval: number;
  imageNumber: number | null;
  imageCount: number;
}

const config: IConfig = {
  api: 'http://palicanon.codebuckets.com.au/api/quote',
  interval: 10000,
  imageNumber: null,
  imageCount: 10,
};

export default config;
