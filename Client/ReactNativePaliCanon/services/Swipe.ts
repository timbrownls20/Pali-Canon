export class Swipe {
  constructor(private X: number, private Y: number) {}
  isRightSwipe() {
    return this.X > 0;
  }
  isLeftSwipe() {
    return this.X < 0;
  }
}
