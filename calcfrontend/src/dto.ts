export interface numbers{
  numbers: string[];
}

export interface CalculatedNumbers{
  calculationId: number;
  userId: string;
  equation: string;
  numbers: number[];
  operation: number;
  result: number;
  dateStamp: Date;
}

export interface NumbersToBeCalculated{
  userId: string;
  equation: string;
  operation: number;
}
