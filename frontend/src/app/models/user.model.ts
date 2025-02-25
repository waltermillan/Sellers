export interface User {
    id: number;
    userName: string;
    passwordHash: string;
    createdAt: Date;
    updatedAt: Date;
}