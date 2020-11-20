export interface ShowTaskShortInfo {
    id: number;
    deadline: string;
    title: string;
    description: string;
    executorName: string;
    statusId: number;
    missedDeadline: boolean;
}
