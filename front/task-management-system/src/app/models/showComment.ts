export interface ShowComment {
    id: number;
    text: string;
    userId: number;
    replyCommentId: number;
    date: string;
    replyUserName: string;
    userName: string;
    replies: ShowComment[];
}