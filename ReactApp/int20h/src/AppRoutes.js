import Projects from "./pages/Projects/Projects";
import ProjectPage from "./pages/ProjectPage/ProjectPage";
import Home from "./pages/Home/Home";
import SignIn from "./pages/SignIn/SignIn";
import SignUp from "./pages/SignUp/SignUp";
import RequireAuth from "./hoc/RequireAuth";
import User from "./pages/User/User"

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <Home />
  },
  {
    path: '/projects',
    element: <Projects />
  },
  {
    path: '/projects/:id',
    element: <RequireAuth><ProjectPage /></RequireAuth>
  },
  {
    path: '/user/:id',
    element: <RequireAuth><User/></RequireAuth>
  },
  {
    path: '/signIn',
    element: <SignIn />
  },
  {
    path: '/signUp',
    element: <SignUp />
  }
];

export default AppRoutes;
