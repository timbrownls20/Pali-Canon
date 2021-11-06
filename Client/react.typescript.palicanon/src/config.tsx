interface IConfig {
    api:string,
    interval:number,
    imageNumber:number | null,
    imageCount:number
}

const config:IConfig = {
    api:'http://palicanon.codebuckets.com.au/api/quote',
    interval: 1500,
    imageNumber:10,
    imageCount:10
}

export default config;