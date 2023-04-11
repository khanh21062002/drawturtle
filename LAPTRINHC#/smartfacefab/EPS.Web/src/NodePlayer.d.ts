declare class NodePlayer {

  static debug(enable: boolean): void;


  static load(cb: () => void): void;


  static asyncLoad(): void;


  useMSE(): void;


   addHeaders(headers: object): void;


  setKeepScreenOn(): void;


  setView(viewId: string): void;


  setScaleMode(mode: number): void;

 
  setBufferTime(bufferTime: number): void;


  setVolume(volume: number): void;


  setTimeout(time: number): void;


  resizeView(width: number, height: number): void;


  onResize(rotate: number): void;


  screenshot(filename: string, format: string, quality: number): void;


  start(url: string): void;


  stop(): void;


  fullscreen(): void;

 
  audioResume(): void;


  clearView(): void;


  release(loseContext: boolean): void;


  on(event: string, listener: (...args: any[]) => void): void;
}
