export const getRubleSuffix = (value: number): string => {
  const mod10 = value % 10;
  const mod100 = value % 100;
  if (mod100 >= 11 && mod100 <= 14) return 'лей';
  if (mod10 === 1) return 'ль';
  if (mod10 >= 2 && mod10 <= 4) return 'ля';
  return 'лей';
};
