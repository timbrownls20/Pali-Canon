interface IConfig {
    api:string,
    interval:number,
    imageNumber:number | null
}

const config:IConfig = {
    api:'http://palicanon.codebuckets.com.au/api/quote',
    interval: 1500,
    imageNumber:null
}

export default config;