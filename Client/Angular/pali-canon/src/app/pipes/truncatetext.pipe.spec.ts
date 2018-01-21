import { TruncateTextPipe } from './truncatetext.pipe';

describe('TruncatetextPipe', () => {
  it('create an instance', () => {
    const pipe = new TruncateTextPipe();
    expect(pipe).toBeTruthy();
  });
});
