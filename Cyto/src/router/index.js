import {createRouter, createWebHashHistory} from "vue-router";

import project from "./routes/research/project.router"
import experiment from "./routes/research/experiment.router"

const routes = [
    {
        path: "/",
        component: () => import("../views/Global"),
        children: [
            {
                path: "research",
                component: () => import("../layouts/ResearchLayout"),
                children: [
                    ...project,
                    ...experiment
                ]
            }
        ]
    },
];

const router = createRouter({
    history: createWebHashHistory(),
    routes
});

export default router;
