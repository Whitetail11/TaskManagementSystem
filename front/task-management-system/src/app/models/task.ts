export interface Task {
    id: number;
    title: string;
    description: string;
    creatorId: string;
    executorId: string;
    date: Date;
    deadline: Date;
    statusId: number;
  }