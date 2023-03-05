
function Button({children, ...props}) {
	return (
		<div>
			<button {...props} className="Button">{children}</button>
		</div>
	);
}

export default Button;
