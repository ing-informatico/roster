import './pill.css';

type PillTone = 'green' | 'gray' | 'blue' | 'amber';

interface PillProps {
  tone: PillTone;
  label: string;
}

export function Pill({ tone, label }: PillProps) {
  return <span className={`pill pill-${tone}`}>{label}</span>;
}
