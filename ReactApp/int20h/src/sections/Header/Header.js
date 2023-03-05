import MyNavLink from "../../atoms/MyNavLink"
import "../../styles/css/index.css"
import { useStateContext } from "../../context/context"

function Header(){

	const {isAuth, name, lastName, hasPhoto, photo} = useStateContext();

	return(
		<header>
			<div className="container navigation">
				<div className="navigation__logo">
					<MyNavLink className="header__link" to={isAuth ? "/user/:id" : "/"}>
						<div className="logo">
							<img></img>
							<span className="logo__text">Work<span className="Land">Wave</span></span>
						</div>
					</MyNavLink>
				</div>
				{isAuth?
				<div className="navigation__main">
					<MyNavLink className="header__link" to="/projects">Проєкти</MyNavLink>
					<MyNavLink className="header__link" to="/user/:id">Профіль</MyNavLink>
				</div>
				: null
				}
					{isAuth
					?
					<div className="navigation__user">
						<div className="username">{name}<span> {lastName}</span></div>
						<img className="avatar" src={require(hasPhoto ? "../../assets/avatar.png" : "../../assets/avatar.png")}></img>
					</div>	
					:<MyNavLink className="header__link user" to="/signIn">Увійти</MyNavLink>
					}
			 </div>
		</header>
	)
}

export default Header