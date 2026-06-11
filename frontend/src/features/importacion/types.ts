export interface ImportError {
  fila: number;
  motivo: string;
}

export interface ImportResult {
  totalFilas: number;
  insertados: number;
  actualizados: number;
  omitidos: number;
  errores: ImportError[];
}
