type Descriptor<T> = {
  [P in keyof T]: (v: any) => T[P];
};

export function Pick<T>(v: any, d: Descriptor<T>): T {
  const ret: any = {};
  for (let key in d) {
    try {
      const val = d[key](v[key]);
      if (typeof val !== 'undefined') {
        ret[key] = val;
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : String(err);
      throw new Error(`could not pick ${key}: ${msg}`);
    }
  }
  return ret;
}
