import { ShowComment } from './showComment';

export interface ShowTask {
    id: number;
    title: string;
    description: string;
    statusId: number;
    deadline: string;
    creatorName: string;
    executorName: string;
    comments: ShowComment[];
}